using DevExpress.CodeParser;
using DevExpress.Spreadsheet;
using DevExpress.Utils.Text;
using DevExpress.XtraPrinting.Export;
using StackExchange.Redis;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Constants;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages.Admin;
using TDITimeSheet.Pages.ContractLine;
using TDITimeSheet.Pages.Leave;
using static System.Net.Mime.MediaTypeNames;

namespace TDITimeSheet.Data
{
    public class CommonController
    {
        private ICommonService _commonService;
        private IEmailSender _sendEmailService;
        private  HttpClient _httpClient;
        public CommonController(ICommonService commonService, IEmailSender sendEmailService, HttpClient httpClient)
        {
            this._commonService = commonService;
            _sendEmailService = sendEmailService;
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
           var rs =  await _httpClient.PostAsJsonAsync(url, data);
            return rs;
        }
        public async Task<GenericResult> GetFunctionByRole(string Role)
        {
            return await _commonService.GetFunctionByRole(Role);
        }

        public bool CheckPermission(string Action, List<RolePermission> permissionList)
        {
            bool result = false;
            //if (string.IsNullOrEmpty(FunctionId))
            //{

            if (permissionList != null && permissionList.Count() > 0)
            {
                var checkValue = permissionList.Where(x => x.Permission == Action);
                if (checkValue != null && checkValue.Count() > 0)
                {
                    result = true;
                }
            }

            //}
            //else
            //{
            //    if()
            //    if (permissionList != null && permissionList.Count() > 0)
            //    {
            //        var checkValue = permissionList.Where(x => x.Permission == Action);
            //        if (checkValue != null && checkValue.Count() > 0)
            //        {
            //            result = true;
            //        }
            //    }

            //}

            return result;
        }

        public async Task<GenericResult> GetRolePermissionByRoleAndFunction(string Role, string Function)
        {


            return await _commonService.GetRolePermissionByRoleAndFunction(Role, Function);

        }

        public DataSet ReadExcelData(Stream fileStream)
        {
            DataSet ds = new DataSet();
            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadDocument(fileStream);

                ds = ConvertExcelToDataSet(workbook, true);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        private DataSet ConvertExcelToDataSet(Workbook _source, bool _useFirstRowAsColumns)
        {
            DataSet ds = new DataSet();
            int _startRow = 0;
            if (_useFirstRowAsColumns)
            {
                _startRow = 1;
            }
            ds = ConvertExcelToDataSet(_source, _startRow);

            return ds;
        }

        private DataSet ConvertExcelToDataSet(Workbook _source, int _startRow = 0)
        {
            DataSet ds = new DataSet();
            foreach (Worksheet _exl in _source.Worksheets)
            {
                DataTable _dt = new DataTable(_exl.Name);
                CellRange _usedRange = _exl.GetUsedRange();
                //_startRow = 0;
                //if (_useFirstRowAsColumns)
                //{
                //    _startRow = 1;
                //}
                // set the columns defined
                for (int _ix = 0; _ix < _usedRange.ColumnCount; _ix++)
                {
                    if (_startRow == 1)
                    {
                        _dt.Columns.Add(_exl.Cells[0, _ix].DisplayText);
                    }
                    else
                    {
                        _dt.Columns.Add(string.Format("Column{0}", _ix));
                    }
                }
                // add the data
                for (int _irow = _startRow; _irow < _usedRange.RowCount; _irow++)
                {
                    if (_startRow == 1 && string.IsNullOrEmpty(_exl.Cells[_irow, 0].DisplayText))
                    {
                        continue;
                    }

                    DataRow _row = _dt.NewRow();
                    for (int _icol = 0; _icol < _usedRange.ColumnCount; _icol++)
                    {
                        _row[_icol] = _exl.Cells[_irow, _icol].DisplayText;
                    }
                    _dt.Rows.Add(_row);
                }

                ds.Tables.Add(_dt);
            }
            return ds;
        }

        public async Task<GenericResult> LeaveSendEmail(LeaveModel leave)
        {
            GenericResult result = new GenericResult();

            string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
            string tempath = rootpath + "\\Templates\\EmailTemplate\\LeaveTemplate.html";
            string emailContent = File.ReadAllText(tempath);
            if (!string.IsNullOrEmpty(emailContent))
            {
                var users = await _commonService.GetUsersForMailLeaveAsync(leave.Username);

                if (users.Count > 0)
                {
                    string action = leave.LeaveStatus == LeaveStatusType.New ? "Added" : leave.LeaveStatus;
                    if (leave.LeaveStatus == LeaveStatusType.New && leave.ModifiedBy == leave.Username)
                    {
                        action = "updated";
                    }

                    string ismorning = leave.AllDay ? "Both" : (leave.IsMorning ? "Morning" : "Afternoon");

                    emailContent = emailContent.Replace("{usercode}", leave.Username);
                    emailContent = emailContent.Replace("{byapprover}", string.Empty);
                    emailContent = emailContent.Replace("{action}", action);
                    emailContent = emailContent.Replace("{id}", leave.Id.ToString());
                    emailContent = emailContent.Replace("{status}", leave.LeaveStatus);
                    emailContent = emailContent.Replace("{leavefrom}", leave.LeaveFrom.ToString("dd-MM-yyyy"));
                    emailContent = emailContent.Replace("{leaveto}", leave.LeaveTo.ToString("dd-MM-yyyy"));
                    emailContent = emailContent.Replace("{allday}", leave.AllDay ? "Yes" : "No");
                    emailContent = emailContent.Replace("{ismorning}", ismorning);
                    emailContent = emailContent.Replace("{leavetype}", leave.LeaveType);
                    emailContent = emailContent.Replace("{reason}", leave.Reason);
                    emailContent = emailContent.Replace("{remark}", leave.Remark);

                    string subject = $"TDI Leave Application – {leave.Username}";
                    List<string> cc = new List<string>();
                    string starffEmail = string.Empty;
                    foreach (UserModel user in users)
                    {
                        if (!string.IsNullOrEmpty(user.Email))
                        {
                            if (user.UserName == leave.Username)
                            {
                                starffEmail = user.Email;
                                cc.Add(leave.HREmail);
                            }
                            else
                            {
                                cc.Add(user.Email);
                            }
                        }
                        if (!string.IsNullOrEmpty(user.LeaderEmail))
                        {
                            cc.Add(user.LeaderEmail);
                        }
                    }

                    //if (!cc.Contains(_sendEmailService.EmailSettings.AdminTeamEmail))
                    //    cc.Add(_sendEmailService.EmailSettings.AdminTeamEmail);
                    

                    await _sendEmailService.SendEmailAsync(starffEmail, subject, emailContent, cc);
                }
            }

            return result;

        }
        public async Task<GenericResult> WBSSendmail(ContractLineModel contractLine, string toemail)
        {
            GenericResult result = new GenericResult();
            try
            {
                List<string> cc = new List<string>();
               //string fromEmail = "dat.n@tdiapj.com";
                //cc.Add("dat.n@tdiapj.com");
                string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
                string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestWBSTemplate.html";
                string emailContent = File.ReadAllText(tempath);
                //emailContent = emailContent.Replace("{projectCode}", "Interval timesheet");
                //emailContent = emailContent.Replace("{WBSCode}", "Dev");
                string subject = $"Project - WBS have been expired or not enough mandays";
                emailContent = emailContent.Replace("{PM}", contractLine.PM);
                emailContent = emailContent.Replace("{UserName}", contractLine.UserName);
                emailContent = emailContent.Replace("{projectCode}", contractLine.ContractName);
                emailContent = emailContent.Replace("{WBSCode}", contractLine.LineCode);

                await _sendEmailService.SendEmailAsync(toemail, subject, emailContent, cc);
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult> WBSByUserSendmail(string  projectCode, string lineCode, DateTime fromDate, DateTime toDate, string toemail,string PM)
        {
            GenericResult result = new GenericResult();
            try
            {
                List<string> cc = new List<string>();
                //string fromEmail = "dat.n@tdiapj.com";
                //cc.Add("dat.n@tdiapj.com");
                string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
                string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestWBSByUserTemplate.html";
                string emailContent = File.ReadAllText(tempath);
                //emailContent = emailContent.Replace("{projectCode}", "Interval timesheet");
                //emailContent = emailContent.Replace("{WBSCode}", "Dev");
                //string subject = $"Project - WBS have been expired or not enough mandays";
                string subject = $"Assigned to Project";
                //emailContent = emailContent.Replace("{UserName}", );
                emailContent = emailContent.Replace("{projectCode}", projectCode);
                emailContent = emailContent.Replace("{WBSCode}", lineCode);
                emailContent = emailContent.Replace("{fromDate}", fromDate.ToString());
                emailContent = emailContent.Replace("{toDate}", toDate.ToString());
                emailContent = emailContent.Replace("{PM}", PM);
                await _sendEmailService.SendEmailAsync(toemail, subject, emailContent, cc);
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }






        //public async Task<GenericResult> WBSSendmailList(List<WBSHeaderModel> wbsList)
        //{
        //    GenericResult result = new GenericResult();

        //    foreach (var wbsLine in wbsList)
        //    {
        //        List<string> cc = new List<string>();
        //        //cc.Add("dat.n@tdiapj.com");
        //        string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
        //        string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestWBSTemplate.html";
        //        string emailContent = File.ReadAllText(tempath);
        //        //emailContent = emailContent.Replace("{projectCode}", "Interval timesheet");
        //        //emailContent = emailContent.Replace("{WBSCode}", "Dev");
        //        string subject = $"Project - WBS have been expired or not enough mandays";
        //        emailContent = emailContent.Replace("{PM}", wbsLine.PM);
        //        emailContent = emailContent.Replace("{projectCode}", wbsLine.ContractName);
        //        emailContent = emailContent.Replace("{WBSCode}", wbsLine.LineCode);
        //        string email = wbsLine.EmailPM;
        //        await _sendEmailService.SendEmailAsync(email, subject, emailContent, cc);
        //    }

        //    return result;
        //}



        //public async Task<GenericResult> FriSendMail()
        //{
        //    GenericResult result = new GenericResult();
        //    List<string> cc = new List<string>();
        //    cc.Add("dat.n@tdiapj.com");
        //    string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
        //    string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestTimeEntryTemplate.html";
        //    string emailContent = File.ReadAllText(tempath);
        //    await _sendEmailService.SendEmailAsync("nguyentiendat.pick@gmail.com", "Time Entry", emailContent, cc);
        //    return result;

        //}

        //public GenericResult testSendMailT6()
        //{

        //    GenericResult result = new GenericResult();

        //    try
        //    {
        //        List<string> cc = new List<string>();
        //        cc.Add("dat.n@tdiapj.com");
        //        string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot");
        //        //string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestTimeEntryTemplate.html";
        //        string tempath = "C:\\Users\\ADMIN\\source\\repos\\TDI TimeSheet 2023\\TimeSheet.Intergration\\wwwroot\\Templates\\EmailTemplate\\RequestTimeEntryTemplate.html";
        //        string emailContent = File.ReadAllText(tempath);
        //        _sendEmailService.SendEmailAsync("nguyentiendat.pick@gmail.com", "Time Entry", emailContent, cc);
        //        result.Success = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;
        //    }

        //    return result;


        //}



    }
}

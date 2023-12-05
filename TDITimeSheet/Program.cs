using AutoMapper;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using TDI.Application.AutoMapper;
using TDI.Application.Implements;
using TDI.Application.Implements.RPT;
using TDI.Application.Interfaces;
using TDI.Application.Interfaces.RPT;
using TDI.Data.Entities;
using TDI.Data.Infrastructure;
using TDI.Data.Models;
using TDI.Data.Repositories;
using TDITimeSheet.Authentication;
using TDITimeSheet.Data;
using TDITimeSheet.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddDevExpressBlazor(cfg => cfg.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v4);
builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredModal();
builder.Services.AddHttpClient();
//Gửi Gmail
// Bắt buộc có
//Init Mapper -~~~~~~
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DomainToViewModelMappingProfile());
    cfg.AddProfile(new ViewModelToDomainMappingProfile());
});
// ~~~~~~

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailSender, EmailSender>();


// Init repository
builder.Services.AddScoped<ConnectionFactory>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Kết thúc Init Repository và mapper




//Kết nối Interface và Service ở tầng Application
builder.Services.AddTransient<ICountryService, TDI.Application.Implements.CountryService>();
builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddTransient<ITimeEntryService, TimeEntryService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<ITestService, TestService>();
builder.Services.AddTransient<ILeaveService, LeaveService>();
builder.Services.AddTransient<IContractLineService, ContractLineService>();
builder.Services.AddTransient<IContractHeaderService, ContracHeaderService>();
builder.Services.AddTransient<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddTransient<IContractAssignMemberService, ContractAssignMemberService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IContractMemberService, ContractMemberService>();
builder.Services.AddTransient<rpt_IApprovalTimesheetSummaryReport, rpt_ApprovalTimesheetSummary>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<IHolidayService, HolidayService>();
builder.Services.AddTransient<ILeaveBalanceService, LeaveBalanceService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IChangePasswordService, ChangePasswordService>();
builder.Services.AddTransient<ITeamMemberService, TeamMemberService>();
builder.Services.AddTransient<IOffInLieuService, OffInLieuService>();









//Khai báo tầng data để sử trên giao diện
builder.Services.AddTransient<CommonController>();
builder.Services.AddTransient<CountryController>();

builder.Services.AddTransient<UserAccountController>();
builder.Services.AddTransient<TimeEntryController>();
builder.Services.AddTransient<ProjectController>();
builder.Services.AddTransient<TestController>(); 
builder.Services.AddTransient<LeaveController>();
builder.Services.AddTransient<ContractLineController>();
builder.Services.AddTransient<ContractHeaderController>();
builder.Services.AddTransient<ContractAssignMemberController>();
builder.Services.AddTransient<UserController>();
builder.Services.AddTransient<ContractMemberController>();
builder.Services.AddTransient<ApprovalTimeSheetSummaryController>();
builder.Services.AddTransient<RolesController>();
builder.Services.AddTransient<PermissionController>();
builder.Services.AddTransient<ChangePasswordController>();
builder.Services.AddTransient<TeamController>();
builder.Services.AddTransient<TeamMemberController>();
builder.Services.AddTransient<HolidayController>();










builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();

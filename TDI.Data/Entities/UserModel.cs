using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Utilities.Constants;

namespace TDI.Data.Entities
{
    public class UserModel
    {
        private bool active;
        private string status;
        private string userPassword;
        private string hashPassword;

        public Guid Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Balance { get; set; }
        public string Avatar { get; set; }
        public string HREmail { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get => status; set => status = value; }
        public string UserPassword { get => userPassword; set => userPassword = value; }
        public string HashPassword
        {
            get
            {
                hashPassword = TDI.Utilities.Helpers.Encryptor.DecryptString(this.userPassword, TDI.Utilities.Constants.AppConstants.TEXT_PHRASE);
                return hashPassword;
            }
            set
            {
                hashPassword = value;
                this.userPassword= TDI.Utilities.Helpers.Encryptor.EncryptString(this.hashPassword, TDI.Utilities.Constants.AppConstants.TEXT_PHRASE);
            }
        }
        public string EmployeeId { get; set; }
        public string CustomF1 { get; set; }
        public string CustomF2 { get; set; }
        public string CustomF3 { get; set; }
        public string CustomF4 { get; set; }
        public string UserType { get; set; }
        public string UserCode { get; set; }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int TeamId { get; set; }
        public int LeaveCountry { get; set; }
        public string TeamName { get; set; }
        public bool Active
        {
            get
            {
                
                if (status == TSStatus.Active)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
                return active;
            }

            set
            {
                active = value;
                if (active == true)
                {
                    status = TSStatus.Active;
                }
                else
                {
                    status = TSStatus.InActive;
                }
            }
        }
        public string LeaderEmail { get; set; }
    }
}

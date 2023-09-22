using RealEstateAgency.Enums;

namespace RealEstateAgency.Models
{
    public class AcessToActions
    {
        public int UserIdUser { get; set; }
        public User User { get; set; }
        public EnumUserRanked UserTypeAccount { get; set; }
        public bool GetInformationYourSelf { get; set; } = false;
        public bool GetYourOrders { get; set; } = false;
        public bool GetListAllOrders { get; set; } = false;
        public bool GetListAllClients { get; set; } = false;
        public bool GetListAllWorkers { get; set; } = false;
        public bool GetListAllAdmins { get; set; } = false;
        public bool SetScoreForOrders { get; set; } = false;
        //Change yourself
        public bool ChangeYourUserName { get; set; } = false;
        public bool ChangeYourPassword { get; set; } = false;
        public bool ChangeYourEmail { get; set; } = false;
        public bool ChangeYourFirstName { get; set; } = false;
        public bool ChangeYourLastName { get; set; } = false;
        public bool ChangeYourSecondName { get; set; } = false;     
        //Change another user
        public bool ChangeAnotherUserId { get; set; } = false;
        public bool ChangeAnotherUserName { get; set; } = false;
        public bool ChangeAnotherPassword { get; set; } = false;
        public bool ChangeAnotherEmail { get; set; } = false;
        public bool ChangeAnotherFirstName { get; set; } = false;
        public bool ChangeAnotherLastName { get; set; } = false;
        public bool ChangeAnotherSecondName { get; set; } = false;
        public bool ChangeAnotherStatus { get; set; } = false;
        public bool AcceptOrdersClient { get; set; } = false;
        public bool CreateOrder { get; set; } = false;
        public bool IneractionWithShifts { get; set; } = false;
        public bool CreateService { get; set; } = false;
        public bool EditService { get; set; } = false;
        public bool DeleteService { get; set; } = false;
        public bool GiveRoleClient { get; set; } = false;
        public bool GiveRoleWorker { get; set; } = false;
        public bool GiveRoleAdmin { get; set; } = false;
        public bool DeleteUser { get; set; } = false;
        public bool DeleteWorker { get; set; } = false;
        public bool DeleteClient { get; set; } = false;
        public bool AcceptNewWorker { get; set; } = false;
        public bool FireAnEmployeWorker { get; set; } = false;
        public bool AcceptNewAdmin { get; set; } = false;
        public bool FireAnEmployeAdmin { get; set; } = false;
        public bool DeleteAdmin { get; set; } = false;
        public bool MakeAnyActions { get; set; } = false;
        public bool GetSalaryAnyWorkers { get; set; } = false;
        public bool AddSalayWorkersForMonth { get; set; } = false;
    }
}

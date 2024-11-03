using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData
{
    public class User
    {
        public int Id { get; set; }
        public string UserPassword { internal get; set; }
        public string UserAdmin {  get; set; }
        public string UserName { get; set; }
        public string UserMiddleName {  get; set; }
        public string UserLastName {  get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserAddress {  get; set; }
        public string UserGroup {  get; set; }
    }
}

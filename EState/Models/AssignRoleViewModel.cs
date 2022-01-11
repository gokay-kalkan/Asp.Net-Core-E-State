using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Models
{
    
        public class AssignRoleViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool IsAssign { get; set; }
        }
        public class UserAssignRoleViewModel
        {
            public string UserId { get; set; }
            public List<AssignRoleViewModel> AssignRoleViewModel { get; set; }

            public UserAssignRoleViewModel()
            {
                AssignRoleViewModel = new List<AssignRoleViewModel>();

            }
        }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Member
{
    internal class StudentMember : Member
    {
        readonly string _studentId = CustomUtils.GenerateUniqueID(0, 8);
        public string StudentId { get => _studentId; }

        public StudentMember(string firstName, string lastName, string email) : base(firstName: firstName, lastName: lastName, email: email, type: MemberType.Student)
        {

        }

    }
}

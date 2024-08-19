﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Member
{
    internal class TeacherMember : Member
    {
        readonly string _teacherId = CustomUtils.GenerateUniqueID(0, 8);
        public string TeacherId { get => _teacherId; }

        public TeacherMember(string firstName, string lastName, string email) : base(firstName: firstName, lastName: lastName, email: email, type: MemberType.Teacher)
        {

        }
    }
}
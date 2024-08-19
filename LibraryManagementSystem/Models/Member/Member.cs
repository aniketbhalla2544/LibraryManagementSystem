﻿using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Member
{
    internal abstract class Member
    {
        public enum MemberType
        {
            None,
            Student,
            Teacher
        }

        const int MEMBER_ID_LENGTH = 8;

        readonly string _memberId = CustomUtils.GenerateUniqueID(0, MEMBER_ID_LENGTH);
        string _firstName = string.Empty;
        string _lastName = string.Empty;
        string _email = string.Empty;
        MemberType _type = MemberType.None;

        protected HashSet<string> UniqueBorrowedBookIds { get; set; } = new HashSet<string>();
        public MemberType Type
        {
            get => _type;
            protected set
            {
                if (value.Equals(MemberType.None))
                    throw new ArgumentException("Can't set member type to 'None' while creating member");
                _type = value;
            }
        }
        public string Name { get => $"{FirstName} {LastName}"; }
        public string MemberId { get => _memberId; }
        public string FirstName
        {
            get => _firstName;
            protected set => _firstName = !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) ? value : string.Empty;
        }
        public string LastName
        {
            get => _lastName;
            protected set => _lastName = !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) ? value : string.Empty;
        }
        public string Email { get => _email; set => _email = Validator.IsValidEmail(value) ? value.Trim().ToLower() : string.Empty; }

        protected Member(string firstName, string lastName, string email, MemberType type)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Type = type;
        }

        public override string ToString()
        {
            return $"Member details:\n\tid: '{MemberId}'" +
                $"\n\tname: '{Name}'" +
                $"\n\temail: '{Email}'" +
                $"\n\ttype: '{Type}'";
        }
    }
}
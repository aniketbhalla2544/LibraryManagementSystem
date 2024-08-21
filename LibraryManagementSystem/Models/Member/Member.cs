using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Models.Member
{
    // uniqueness = email
    internal abstract class Member
    {
        public enum MemberType
        {
            Student,
            Teacher
        }

        const int MEMBER_ID_LENGTH = 8;

        readonly string _memberId = CustomUtils.GenerateUniqueID(0, MEMBER_ID_LENGTH);
        string _firstName = string.Empty;
        string _lastName = string.Empty;
        string _email = string.Empty; // unique prop
        MemberType _type;

        protected HashSet<string> UniqueBorrowedBookIds { get; set; } = new HashSet<string>();
        public readonly static List<string> MemberTypeNames = Enum.GetNames(typeof(MemberType)).ToList();
        public MemberType Type { get => _type; protected set => _type = value; }
        public string Name { get => $"{FirstName} {LastName}"; }
        public string MemberId { get => _memberId; }
        public string FirstName
        {
            get => _firstName;
            protected set => _firstName = !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) ? value.Trim().ToLower() : string.Empty;
        }
        public string LastName
        {
            get => _lastName;
            protected set => _lastName = !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value) ? value.Trim().ToLower() : string.Empty;
        }
        public string Email
        {
            get => _email;
            private set
            {
                if (!Validator.IsValidEmail(value))
                    throw new ArgumentException($"Can't set an invalid email: '{value}' while creating system member.");

                _email = value.Trim().ToLower();
            }
        }

        protected Member(string firstName, string lastName, string email, MemberType type)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Type = type;
        }

        // uniqueness = email
        public override bool Equals(object obj) => obj is Member member && Email.Equals(member.Email.Trim(), StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() => Email.GetHashCode();

        public override string ToString()
        {
            return $"Member details:\n\tid: '{MemberId}'" +
                $"\n\tname: '{Name}'" +
                $"\n\temail: '{Email}'" +
                $"\n\ttype: '{Type}'";
        }
    }
}

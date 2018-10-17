using System;
using SqlRepoEx.Core.CustomAttribute;

namespace GettingStartedStatic
{
    public class ToDo
    {
        [NonDatabaseField]
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }


        public string Task { get; set; }

        [IdentityField]
        public int Id { get; set; }
    }


    public class TaskRemark
    {

        public string Remark { get; set; }
        [KeyField]
        public string Task { get; set; }
        [IdentityField]
        public int Id { get; set; }
    }
}
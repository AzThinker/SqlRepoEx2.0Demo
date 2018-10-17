using System;
using SqlRepoEx.Core.CustomAttribute;

namespace GettingStartedMySql
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
        public string Task { get; set; }

        public int Id { get; set; }
    }
}
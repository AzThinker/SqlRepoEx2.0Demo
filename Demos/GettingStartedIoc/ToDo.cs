using System;
using System.ComponentModel.DataAnnotations;
using SqlRepoEx.Core;
using SqlRepoEx.Core.CustomAttribute;

namespace GettingStartedIoC
{
    [TableName("ToDo")]
    public class ToDo_New
    {
        [NonDatabaseField]
        public DateTime CreatedDate { get; set; }


        public bool IsCompleted { get; set; }
        public string Task { get; set; }

        [IdentityField]
        public int Id { get; set; }
    }

    [TableName("DoitTest")]
    public class DoitTest_New
    {
        [IdentityField]
        public int TestId { get; set; }

        public string TestRmk { get; set; }


        public bool TestBool { get; set; }

        [NonDatabaseField]
        public string tablestr { get; set; }
    }

}
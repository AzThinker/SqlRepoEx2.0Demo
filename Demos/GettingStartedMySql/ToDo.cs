using System;


namespace GettingStartedMySql
{
    public class ToDo
    {
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Task { get; set; }

        public int Id { get; set; }
    }

    public class TaskRemark
    {

        public string Remark { get; set; }
        public string Task { get; set; }

        public int Id { get; set; }
    }
}
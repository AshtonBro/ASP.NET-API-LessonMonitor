﻿using System;

namespace LessonMonitor.DataAccess.MSSQL.Entities
{
    public class GitHubAccount
    {
        public int MemberId { get; set; }

        public string Nickname { get; set; }

        public Uri Link { get; set; }

        public int GithubAccountId { get; set; }

        public Member Member { get; set; }
    }
}

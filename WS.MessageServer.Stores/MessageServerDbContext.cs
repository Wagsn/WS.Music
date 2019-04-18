﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WS.MessageServer.Entities;

namespace WS.MessageServer.Stores
{
    public class MessageServerDbContext : DbContext
    {

        public MessageServerDbContext(DbContextOptions<MessageServerDbContext> options)
            : base(options) { }

        public DbSet<SendStatus> SendStatus { get; set; }

        public DbSet<SendRecord> SendRecord { get; set; }

        public DbSet<MessageRecord> MessageRecord { get; set; }
    }
}

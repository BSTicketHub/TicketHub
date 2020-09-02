namespace TicketHubDataLibrary.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TicketHubContext : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'TicketHubContext' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'TicketHubDataLibrary.Models.TicketHubContext' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'TicketHubContext' 連接字串。
        public TicketHubContext()
            : base("name=LocalConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserWishIssue>().HasRequired(wi => wi.Issue).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserWishIssue>().HasRequired(wi => wi.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderDetail>().HasRequired(od => od.Issue).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderDetail>().HasRequired(od => od.Order).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Ticket>().HasRequired(t => t.Issue).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Ticket>().HasRequired(t => t.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Ticket>().HasRequired(t => t.Order).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<RefundDetail>().HasRequired(rd => rd.Refund).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<RefundDetail>().HasRequired(rd => rd.Ticket).WithMany().WillCascadeOnDelete(false);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<ActionLog> ActionLog { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<ShopEmployee> ShopEmployee { get; set; }
        public DbSet<UserFavoriteShop> UserFavoriteShop { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<UserWishIssue> UserWishIssue { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Refund> Refund { get; set; }
        public DbSet<RefundDetail> RefundDetail { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<IssueTag> IssueTag { get; set; }
        public DbSet<ShopTag> ShopTag { get; set; }
    }
}
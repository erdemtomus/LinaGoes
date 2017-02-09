namespace LinaGoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SchoolId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SchoolId");
        }
    }
}

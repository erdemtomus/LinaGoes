namespace LinaGoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolEditedDateAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "EditedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Schools", "EditedBy", c => c.String());
            AlterColumn("dbo.Schools", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schools", "Active", c => c.Byte(nullable: false));
            DropColumn("dbo.Schools", "EditedBy");
            DropColumn("dbo.Schools", "EditedDate");
        }
    }
}

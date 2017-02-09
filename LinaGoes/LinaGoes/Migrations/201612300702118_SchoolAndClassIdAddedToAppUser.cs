namespace LinaGoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolAndClassIdAddedToAppUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(),
                        ClassroomId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        EditedDate = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId, cascadeDelete: true)
                .Index(t => t.ClassroomId);
            
            AddColumn("dbo.AspNetUsers", "SchoolId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ClassroomId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.Students", new[] { "ClassroomId" });
            DropColumn("dbo.AspNetUsers", "ClassroomId");
            DropColumn("dbo.AspNetUsers", "SchoolId");
            DropTable("dbo.Students");
        }
    }
}

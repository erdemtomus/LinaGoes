namespace LinaGoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherClassIntegrationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassroomId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId, cascadeDelete: true)
                .Index(t => t.ClassroomId);
            
            AddColumn("dbo.Schools", "UserId", c => c.String());
            DropColumn("dbo.AspNetUsers", "SchoolId");
            DropColumn("dbo.AspNetUsers", "ClassroomId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ClassroomId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "SchoolId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClassroomTeachers", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.ClassroomTeachers", new[] { "ClassroomId" });
            DropColumn("dbo.Schools", "UserId");
            DropTable("dbo.ClassroomTeachers");
        }
    }
}

namespace LinaGoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassRoomsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Definition = c.String(maxLength: 500),
                        SchoolId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        EditedDate = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .Index(t => t.SchoolId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classrooms", "SchoolId", "dbo.Schools");
            DropIndex("dbo.Classrooms", new[] { "SchoolId" });
            DropTable("dbo.Classrooms");
        }
    }
}

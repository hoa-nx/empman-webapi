namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEmpViewToViewEmpchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmpView", "ID", "dbo.Emps");
            DropIndex("dbo.EmpView", new[] { "ID" });
            CreateTable(
                "dbo.ViewEmp",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Emps", t => t.ID)
                .Index(t => t.ID);
            
            DropTable("dbo.EmpView");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmpView",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.ViewEmp", "ID", "dbo.Emps");
            DropIndex("dbo.ViewEmp", new[] { "ID" });
            DropTable("dbo.ViewEmp");
            CreateIndex("dbo.EmpView", "ID");
            AddForeignKey("dbo.EmpView", "ID", "dbo.Emps", "ID");
        }
    }
}

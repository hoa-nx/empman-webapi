namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSearchEmpFiltertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchEmpFilters",
                c => new
                    {
                        CompanyID = c.Int(nullable: false),
                        EmpID = c.Int(nullable: false),
                        No = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        RowVersion = c.Binary(),
                        DisplayOrder = c.Int(),
                        AccountData = c.String(maxLength: 256),
                        Note = c.String(),
                        AccessDataLevel = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                        DataStatus = c.Int(),
                        UserAgent = c.String(),
                        UserHostAddress = c.String(),
                        UserHostName = c.String(),
                        RequestDate = c.DateTime(),
                        RequestBy = c.String(),
                        ApprovedDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedStatus = c.Int(),
                    })
                .PrimaryKey(t => new { t.CompanyID, t.EmpID });
            
            AlterColumn("dbo.SystemConfigs", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SystemConfigs", "ShortName", c => c.String(maxLength: 256));
            DropTable("dbo.SearchEmpFilters");
        }
    }
}

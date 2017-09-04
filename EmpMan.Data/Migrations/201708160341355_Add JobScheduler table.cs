namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobSchedulertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobSchedulers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobType = c.String(maxLength: 256),
                        Name = c.String(maxLength: 256),
                        ShortName = c.String(maxLength: 256),
                        TableNameRelation = c.String(maxLength: 1024),
                        TableKey = c.String(maxLength: 1024),
                        TableKeyID = c.String(maxLength: 1024),
                        ScheduleRunJobDate = c.DateTime(),
                        JobContent = c.String(),
                        FromEmail = c.String(),
                        ToNotiEmailList = c.String(),
                        CcNotiEmailList = c.String(),
                        BccNotiEmailList = c.String(),
                        SMSFromNumber = c.String(),
                        SMSToNumber = c.String(),
                        SMSContent = c.String(),
                        JobStatus = c.Int(),
                        ActualRunJobDate = c.DateTime(),
                        TemplateID = c.Int(),
                        AttachmementID = c.Int(),
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
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobSchedulers");
        }
    }
}

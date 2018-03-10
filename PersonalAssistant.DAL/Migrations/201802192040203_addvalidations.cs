namespace PersonalAssistant.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvalidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "PersonalID", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.People", "EngFirstName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.People", "EngLastName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.People", "PasswordHash", c => c.String(nullable: false));
            AlterColumn("dbo.Meetings", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meetings", "Name", c => c.String());
            AlterColumn("dbo.People", "PasswordHash", c => c.String());
            AlterColumn("dbo.People", "EngLastName", c => c.String());
            AlterColumn("dbo.People", "EngFirstName", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
            AlterColumn("dbo.People", "PersonalID", c => c.String());
        }
    }
}

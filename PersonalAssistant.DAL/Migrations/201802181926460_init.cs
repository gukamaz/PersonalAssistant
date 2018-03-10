namespace PersonalAssistant.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        SpentMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PersonID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonalID = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EngFirstName = c.String(),
                        EngLastName = c.String(),
                        BirtDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        PasswordHash = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Budgets", t => t.BudgetID, cascadeDelete: true)
                .Index(t => t.BudgetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "BudgetID", "dbo.Budgets");
            DropForeignKey("dbo.Meetings", "PersonID", "dbo.People");
            DropForeignKey("dbo.Budgets", "PersonID", "dbo.People");
            DropIndex("dbo.Transactions", new[] { "BudgetID" });
            DropIndex("dbo.Meetings", new[] { "PersonID" });
            DropIndex("dbo.Budgets", new[] { "PersonID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Meetings");
            DropTable("dbo.People");
            DropTable("dbo.Budgets");
        }
    }
}

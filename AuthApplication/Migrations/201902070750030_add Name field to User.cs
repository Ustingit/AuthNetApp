namespace AuthApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNamefieldtoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}

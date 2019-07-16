namespace watchApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class babis : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "Director_Id", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "Director_Id" });
            RenameColumn(table: "dbo.Movies", name: "Category_Name", newName: "Genre");
            RenameColumn(table: "dbo.Movies", name: "Director_Id", newName: "DirectorId");
            RenameIndex(table: "dbo.Movies", name: "IX_Category_Name", newName: "IX_Genre");
            AlterColumn("dbo.Movies", "DirectorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "DirectorId");
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            AlterColumn("dbo.Movies", "DirectorId", c => c.Int());
            RenameIndex(table: "dbo.Movies", name: "IX_Genre", newName: "IX_Category_Name");
            RenameColumn(table: "dbo.Movies", name: "DirectorId", newName: "Director_Id");
            RenameColumn(table: "dbo.Movies", name: "Genre", newName: "Category_Name");
            CreateIndex("dbo.Movies", "Director_Id");
            AddForeignKey("dbo.Movies", "Director_Id", "dbo.Directors", "Id");
        }
    }
}

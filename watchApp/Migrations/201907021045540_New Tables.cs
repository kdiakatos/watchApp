namespace watchApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            AddColumn("dbo.Movies", "Category_Name", c => c.String(maxLength: 128));
            AddColumn("dbo.Movies", "Director_Id", c => c.Int());
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.Movies", "Category_Name");
            CreateIndex("dbo.Movies", "Director_Id");
            AddForeignKey("dbo.Movies", "Category_Name", "dbo.Categories", "Name");
            AddForeignKey("dbo.Movies", "Director_Id", "dbo.Directors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Director_Id", "dbo.Directors");
            DropForeignKey("dbo.Movies", "Category_Name", "dbo.Categories");
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropIndex("dbo.Movies", new[] { "Director_Id" });
            DropIndex("dbo.Movies", new[] { "Category_Name" });
            AlterColumn("dbo.Movies", "Title", c => c.String());
            DropColumn("dbo.Movies", "Director_Id");
            DropColumn("dbo.Movies", "Category_Name");
            DropTable("dbo.MovieActors");
            DropTable("dbo.Directors");
            DropTable("dbo.Categories");
            DropTable("dbo.Actors");
        }
    }
}

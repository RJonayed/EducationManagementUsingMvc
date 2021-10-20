namespace AuthUsingIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentsEn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        AdmissionID = c.Int(nullable: false, identity: true),
                        clsRoll = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        StudentsId = c.Int(nullable: false),
                        CourseDtlId = c.Int(nullable: false),
                        ModuleDtlId = c.Int(nullable: false),
                        AdsCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CourseCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdsDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        TspId = c.Int(nullable: false),
                        StudentInformation_StudentId = c.Int(),
                        TspInfo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.AdmissionID)
                .ForeignKey("dbo.CourseDtls", t => t.CourseDtlId, cascadeDelete: true)
                .ForeignKey("dbo.ModuleDtls", t => t.ModuleDtlId, cascadeDelete: true)
                .ForeignKey("dbo.StudentInformations", t => t.StudentInformation_StudentId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.TspInfoes", t => t.TspInfo_ID)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseDtlId)
                .Index(t => t.ModuleDtlId)
                .Index(t => t.StudentInformation_StudentId)
                .Index(t => t.TspInfo_ID);
            
            CreateTable(
                "dbo.CourseDtls",
                c => new
                    {
                        CourseDtlId = c.Int(nullable: false, identity: true),
                        CrsName = c.String(),
                        CrsDuratin = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        ShiftName = c.String(),
                        CrsFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CourseDtlId)
                .ForeignKey("dbo.Shifts", t => t.ShiftId, cascadeDelete: true)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.ModuleDtls",
                c => new
                    {
                        ModuleDtlId = c.Int(nullable: false, identity: true),
                        CourseDtlId = c.Int(nullable: false),
                        MdlName = c.String(),
                        MdlFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ModuleDtlId)
                .ForeignKey("dbo.CourseDtls", t => t.CourseDtlId, cascadeDelete: false)
                .Index(t => t.CourseDtlId);
            
            CreateTable(
                "dbo.ResultMakes",
                c => new
                    {
                        ResultMakeId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        MdlMarks = c.String(),
                        MdlPoint = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MdlGrade = c.String(),
                        ResultDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        CourseDtlId = c.Int(nullable: false),
                        ModuleDtlId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultMakeId)
                .ForeignKey("dbo.CourseDtls", t => t.CourseDtlId, cascadeDelete: true)
                .ForeignKey("dbo.ModuleDtls", t => t.ModuleDtlId, cascadeDelete: true)
                .ForeignKey("dbo.StudentInformations", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseDtlId)
                .Index(t => t.ModuleDtlId);
            
            CreateTable(
                "dbo.StudentInformations",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StName = c.String(),
                        Phone = c.String(),
                        DateOfbirth = c.String(),
                        Email = c.String(),
                        Addresss = c.String(),
                        ImageName = c.String(),
                        ImageData = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftId = c.Int(nullable: false, identity: true),
                        ShiftName = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ShiftId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TchName = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        TchPhone = c.Int(nullable: false),
                        TchEmail = c.String(),
                        TchAddress = c.String(),
                        salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            CreateTable(
                "dbo.TspInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TspName = c.String(),
                        TspPhone = c.String(),
                        TspEmail = c.String(),
                        TspAddress = c.String(),
                        SrtDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admissions", "TspInfo_ID", "dbo.TspInfoes");
            DropForeignKey("dbo.Admissions", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CourseDtls", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.ResultMakes", "StudentId", "dbo.StudentInformations");
            DropForeignKey("dbo.Admissions", "StudentInformation_StudentId", "dbo.StudentInformations");
            DropForeignKey("dbo.ResultMakes", "ModuleDtlId", "dbo.ModuleDtls");
            DropForeignKey("dbo.ResultMakes", "CourseDtlId", "dbo.CourseDtls");
            DropForeignKey("dbo.ModuleDtls", "CourseDtlId", "dbo.CourseDtls");
            DropForeignKey("dbo.Admissions", "ModuleDtlId", "dbo.ModuleDtls");
            DropForeignKey("dbo.Admissions", "CourseDtlId", "dbo.CourseDtls");
            DropIndex("dbo.ResultMakes", new[] { "ModuleDtlId" });
            DropIndex("dbo.ResultMakes", new[] { "CourseDtlId" });
            DropIndex("dbo.ResultMakes", new[] { "StudentId" });
            DropIndex("dbo.ModuleDtls", new[] { "CourseDtlId" });
            DropIndex("dbo.CourseDtls", new[] { "ShiftId" });
            DropIndex("dbo.Admissions", new[] { "TspInfo_ID" });
            DropIndex("dbo.Admissions", new[] { "StudentInformation_StudentId" });
            DropIndex("dbo.Admissions", new[] { "ModuleDtlId" });
            DropIndex("dbo.Admissions", new[] { "CourseDtlId" });
            DropIndex("dbo.Admissions", new[] { "TeacherId" });
            DropTable("dbo.TspInfoes");
            DropTable("dbo.Teachers");
            DropTable("dbo.Shifts");
            DropTable("dbo.StudentInformations");
            DropTable("dbo.ResultMakes");
            DropTable("dbo.ModuleDtls");
            DropTable("dbo.CourseDtls");
            DropTable("dbo.Admissions");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineAptitudeTestDB.Entities;

public partial class OnlineAptitudeTestDbContext : DbContext
{
    public OnlineAptitudeTestDbContext()
    {
    }

    public OnlineAptitudeTestDbContext(DbContextOptions<OnlineAptitudeTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminManager> AdminManagers { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateTestDetail> CandidateTestDetails { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionTopic> QuestionTopics { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=vietanh-pc;Initial Catalog=OnlineAptitudeTestDB;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminManager>(entity =>
        {
            entity.HasKey(e => e.AdminManagerId).HasName("PK__AdminMan__8B21DC4901DDFA92");

            entity.ToTable("AdminManager");

            entity.Property(e => e.AdminManagerId).HasColumnName("AdminManagerID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.AdminManagers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdminMana__RoleI__398D8EEE");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answer__3214EC2717B1766E");

            entity.ToTable("Answer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ContentAnswer).HasColumnType("ntext");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Answer__Question__46E78A0C");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539BFCFFD7205A");

            entity.ToTable("Candidate");

            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.EducationDetails)
                .HasColumnType("ntext")
                .HasColumnName("Education_Details");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkExperience)
                .HasColumnType("ntext")
                .HasColumnName("Work_Experience");

            entity.HasOne(d => d.Role).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidate__RoleI__3E52440B");
        });

        modelBuilder.Entity<CandidateTestDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC2722CC7626");

            entity.ToTable("Candidate_Test_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CandidateAnswer).HasColumnType("ntext");
            entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateTestDetails)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidate__Candi__4E88ABD4");

            entity.HasOne(d => d.TestCodeNavigation).WithMany(p => p.CandidateTestDetails)
                .HasForeignKey(d => d.TestCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidate__TestC__4D94879B");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C1D061CCA");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.ContentQuestion).IsUnicode(false);
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Question__TopicI__4316F928");
        });

        modelBuilder.Entity<QuestionTopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Question__022E0F7D95122E73");

            entity.ToTable("QuestionTopic");

            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.TopicName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AAE782AC1");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestCode).HasName("PK__Test__0B0C35F6F8A1A736");

            entity.ToTable("Test");

            entity.Property(e => e.TimeStart).HasColumnType("datetime");
            entity.Property(e => e.TimeToDo).HasColumnName("Time_To_Do");
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestQues__3214EC272BF3CC27");

            entity.ToTable("TestQuestion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestQuest__Quest__4AB81AF0");

            entity.HasOne(d => d.TestCodeNavigation).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.TestCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestQuest__TestC__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

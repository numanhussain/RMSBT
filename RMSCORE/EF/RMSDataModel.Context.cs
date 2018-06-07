﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMSCORE.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BTRMSEntities : DbContext
    {
        public BTRMSEntities()
            : base("name=BTRMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AreaOfInterest> AreaOfInterests { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidateJob> CandidateJobs { get; set; }
        public virtual DbSet<CandidatePhoto> CandidatePhotoes { get; set; }
        public virtual DbSet<CandidateStrength> CandidateStrengths { get; set; }
        public virtual DbSet<Catagory> Catagories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CompensationDetail> CompensationDetails { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<EduBoard> EduBoards { get; set; }
        public virtual DbSet<EduDegreeLevel> EduDegreeLevels { get; set; }
        public virtual DbSet<EduDegreeType> EduDegreeTypes { get; set; }
        public virtual DbSet<EduDetail> EduDetails { get; set; }
        public virtual DbSet<EduInstitute> EduInstitutes { get; set; }
        public virtual DbSet<ExpCareerLevel> ExpCareerLevels { get; set; }
        public virtual DbSet<ExperienceDetail> ExperienceDetails { get; set; }
        public virtual DbSet<ExperienceIndustry> ExperienceIndustries { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<HearAbout> HearAbouts { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<JobDetail> JobDetails { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<MartialStatu> MartialStatus { get; set; }
        public virtual DbSet<MiscellaneousDetail> MiscellaneousDetails { get; set; }
        public virtual DbSet<NotificationDetail> NotificationDetails { get; set; }
        public virtual DbSet<ReferenceDetail> ReferenceDetails { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<Salutation> Salutations { get; set; }
        public virtual DbSet<SkillDetail> SkillDetails { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<V_AppliedJob> V_AppliedJob { get; set; }
        public virtual DbSet<V_Candidate_EduDetail> V_Candidate_EduDetail { get; set; }
        public virtual DbSet<V_Candidate_Exp> V_Candidate_Exp { get; set; }
        public virtual DbSet<V_Candidate_Miscellaneous> V_Candidate_Miscellaneous { get; set; }
        public virtual DbSet<V_Candidate_Reference> V_Candidate_Reference { get; set; }
        public virtual DbSet<V_Candidate_Skills> V_Candidate_Skills { get; set; }
        public virtual DbSet<V_CandidateProfile> V_CandidateProfile { get; set; }
        public virtual DbSet<V_Interview> V_Interview { get; set; }
        public virtual DbSet<V_JobDetail> V_JobDetail { get; set; }
        public virtual DbSet<V_Notification> V_Notification { get; set; }
        public virtual DbSet<V_UserCandidate> V_UserCandidate { get; set; }
    }
}

//using Almanime.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore.ValueGeneration;

//namespace Almanime.Repositories.Configurations;

//public class MemberConfiguration : BaseModelConfiguration<Member>
//{
//    public override void Configure(EntityTypeBuilder<Member> builder)
//    {
//        base.Configure(builder);

//        builder.Property(member => member.ID).HasValueGenerator<GuidValueGenerator>();

//        builder.HasKey(member => new { member.FansubID, member.UserID });
//    }
//}
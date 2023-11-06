using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Dal.Configurations
{
    internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo>
    {
        public void Configure(EntityTypeBuilder<BasicInfo> builder)
        {
            
        }
    }
}

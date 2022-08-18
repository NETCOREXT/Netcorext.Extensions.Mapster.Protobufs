using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Netcorext.Contracts.Protobufs;

namespace Mapster;

public static class TypeAdapterConfigExtension
{
    public static TypeAdapterConfig LoadProtobufConfig(this TypeAdapterConfig config, bool nameMatchingIgnoreCase = false)
    {
        if (nameMatchingIgnoreCase)
            config.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

        config.Default.UseDestinationValue(t => t.SetterModifier == AccessModifier.None &&
                                                t.Type.IsGenericType &&
                                                t.Type.GetGenericTypeDefinition() == typeof(RepeatedField<>));

        config.ForType<Timestamp?, DateTimeOffset>()
              .MapWith(t => t == null ? default : t.ToDateTimeOffset());

        config.ForType<Timestamp?, DateTimeOffset?>()
              .MapWith(t => t == null ? null : t.ToDateTimeOffset());

        config.ForType<DateTimeOffset?, Timestamp?>()
              .MapWith(t => t.HasValue ? Timestamp.FromDateTimeOffset(t.Value) : null);

        config.ForType<Duration?, TimeSpan>()
              .MapWith(t => t == null ? default : t.ToTimeSpan());

        config.ForType<Duration?, TimeSpan?>()
              .MapWith(t => t == null ? null : t.ToTimeSpan());

        config.ForType<TimeSpan?, Duration?>()
              .MapWith(t => t.HasValue ? Duration.FromTimeSpan(t.Value) : null);

        config.ForType<ProtobufDecimal?, decimal>()
              .MapWith(t => t == null ? default : t.ToDecimal());

        config.ForType<ProtobufDecimal?, decimal?>()
              .MapWith(t => t == null ? null : t.ToDecimal());

        config.ForType<decimal, ProtobufDecimal?>()
              .MapWith(t => t.ToProtobufDecimal());

        return config;
    }
}
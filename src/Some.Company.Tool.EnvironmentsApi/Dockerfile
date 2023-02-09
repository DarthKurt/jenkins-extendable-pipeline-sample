##############
### build  ###
##############
ARG sdk_image
ARG base_image=mcr.microsoft.com/dotnet/aspnet:7.0-alpine
FROM ${sdk_image} AS build

WORKDIR /app

COPY . .

RUN \
        dotnet restore \
                ./Some.Company.Tool.EnvironmentsApi.Tests/Some.Company.Tool.EnvironmentsApi.Tests.csproj \
        && dotnet build --nologo --no-restore -v n \
                ./Some.Company.Tool.EnvironmentsApi.Tests/Some.Company.Tool.EnvironmentsApi.Tests.csproj \
        && dotnet test --nologo --no-build \
                ./Some.Company.Tool.EnvironmentsApi.Tests/Some.Company.Tool.EnvironmentsApi.Tests.csproj \
                  -l 'trx' \
                  --results-directory './testResults' \
                  --collect "XPlat Code Coverage" \
                  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover \
        && dotnet publish --nologo \
                --self-contained true \
                -r linux-musl-x64 \
                -p:PublishTrimmed=false \
                -p:PublishSingleFile=true \
                -c Release \
                -o /publish \
                ./Some.Company.Tool.EnvironmentsApi/Some.Company.Tool.EnvironmentsApi.csproj

FROM ${base_image} AS app
# Create group and user
ARG user=api
ARG group=api
ARG uid=1000
ARG gid=1000
ARG home_path="/app"

WORKDIR ${home_path}
RUN set -eux; \
    addgroup --gid ${gid} ${group} \
    && adduser --disabled-password --home ${home_path} --uid ${uid} --ingroup ${group} ${user}

USER ${user}

# Enable globalization APIs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ARG base_image
ARG image_name
ARG branch_name
ARG vcs_ref
ARG vcs_url
ARG build_tag
ARG build_date

LABEL \
    org.opencontainers.image.base.name=${base_image} \
    org.opencontainers.image.created=${build_date} \
    org.opencontainers.image.version=${build_tag} \
    org.opencontainers.image.source=${vcs_url} \
    org.opencontainers.image.revision=${vcs_ref} \
    org.opencontainers.image.url=${url} \
    org.opencontainers.image.title=${image_name} \
    org.opencontainers.image.description="Image for providing environments API service." \
    org.opencontainers.image.vendor="Some Company" \
    com.some.company.maintainer="Tool" \
    com.some.company.vcs.branch=${branch_name}



COPY --from=build /publish .

EXPOSE 80
ENTRYPOINT ["sh", "-c", "./Some.Company.Tool.EnvironmentsApi"]
CMD []


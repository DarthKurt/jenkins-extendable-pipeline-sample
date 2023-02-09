ARG sdk_image
ARG image_name=dotnet-cli-mock
ARG base_image=node:18-alpine3.17

FROM ${sdk_image} AS build

WORKDIR /app

COPY . .

RUN  npm ci \
        && npm run build

ARG image_name
ARG base_image

FROM ${base_image} AS app

LABEL \
    org.opencontainers.image.base.name=${base_image} \
    org.opencontainers.image.created=${build_date} \
    org.opencontainers.image.version=${build_tag} \
    org.opencontainers.image.source=${vcs_url} \
    org.opencontainers.image.revision=${vcs_ref} \
    org.opencontainers.image.url=${url} \
    org.opencontainers.image.title=${image_name} \
    org.opencontainers.image.description="Image for testing dotnet-related tasks." \
    org.opencontainers.image.vendor="Some Company" \
    com.some.company.maintainer="Tool" \
    com.some.company.vcs.branch=${branch_name}

ARG NODE_ENV=production

RUN apk add --update \
        dumb-init

ENV NODE_ENV ${NODE_ENV}

WORKDIR /app
COPY --from=build /app/dist .

USER node

ENTRYPOINT ["dumb-init", "--", "node", "./dotnet.js"]
CMD []
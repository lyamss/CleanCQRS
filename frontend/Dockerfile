FROM oven/bun:1

WORKDIR /app

ARG CONFIG_FILE
COPY $CONFIG_FILE /app/.env
ENV NODE_ENV production

COPY package.json ./

RUN bun install --frozen-lockfile

COPY . ./

RUN bun run build

CMD ["bun", "run", "start"]
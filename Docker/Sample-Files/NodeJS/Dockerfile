# Set the base image for everything else in this file.
FROM node:16.13.0

# Set the virtual directory path and copy the package.json file to it.
WORKDIR /usr/src/app
COPY package*.json ./

# Run 'npm install' command, using the package.json file ust copied.
RUN npm install

COPY *.* ./
COPY src src/
COPY public public/

# Set the execution mode to 'development'. There may be different configuration files in the project
# for production and development. With this mode set, build and run the Node.JS application.
ENV NODE_ENV=development
RUN npm run build

# Set application port to 3000. The application can be accessed at localhost:3000.
EXPOSE 3000

# Run 'npm start' terminal command to start the application.
CMD [ "npm", "start" ]
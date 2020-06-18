# Next.js businsess contact record editing app

Based on:
- https://vercel.com/import/project?template=https://github.com/vercel/next.js/tree/canary/examples/with-typescript
- https://github.com/mui-org/material-ui/tree/master/docs/src/pages/getting-started/templates/dashboard
- https://github.com/mui-org/material-ui/tree/master/docs/src/pages/getting-started/templates/sign-in

Augmented with information from:
- https://nextjs.org/docs/api-routes/dynamic-api-routes
- 

Steps to run:
- open two command windows, and a browser
- start the api
 - in a one of the command windows, navigate to the api folder
 - dotnet restore
 - dotnet run
 - navigate to http://localhost:5000/api/users/1/records/generate to create fake data for a user with user id of 1
- start the app
 - in the other command window, navigate to the app folder
 - npm install
 - npm run dev
 - navigate to http://localhost:3000 in a browser
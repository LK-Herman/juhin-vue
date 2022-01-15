#!/usr/bin/env sh

# abort on errors
set -e

# build
npm run build

# navigate into the build output directory
cd dist

# if you are deploying to a custom domain
# echo 'www.example.com' > CNAME

git init
git add -A
git commit -m 'deploy'
git show-ref
# if you are deploying to https://<USERNAME>.github.io
# git push -f git@github.com:lk-herman/lk-herman.github.io.git main

# if you are deploying to https://<USERNAME>.github.io/<REPO>
git push -f git@github.com:LK-Herman/juhin.git main:gh-pages

# cd -

# https://lk-herman.github.io/Juhin/
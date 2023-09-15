echo "Switching to branch main............"
git checkout main

echo "Building frontend............"
npm run build

echo "Deploying frontend............"
scp -i "C:/Users/thenu/Downloads/temp/integrifylibrary_key.pem" -r build/* zealinbee@98.71.75.120:/var/www/build

echo "Deployed successfully!!!!!!!"
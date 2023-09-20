echo "Switching to branch main............"
git checkout main

echo "Building frontend............"
npm run build

echo "Deploying frontend............"
scp -i "C:/Users/thenu/Downloads/ssh/master-VM_key.pem" -r build/* zealinbee@98.71.53.99:/var/www/integrify-library-test

echo "Deployed successfully!!!!!!!"
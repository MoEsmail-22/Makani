"use strict";

// Function to redirect to property details page
function redirectToPropertyDetails(propertyId, propertyTitle, propertyPrice, propertyLocation) {
    // Encode the parameters to be safe in a URL
    const encodedTitle = encodeURIComponent(propertyTitle || 'Property');
    const encodedPrice = encodeURIComponent(propertyPrice || '');
    const encodedLocation = encodeURIComponent(propertyLocation || '');
    
    // Create the URL with query parameters
    const url = `propertyDetails.html?id=${propertyId}&title=${encodedTitle}&price=${encodedPrice}&location=${encodedLocation}`;
    
    // Redirect to the property details page
    window.location.href = url;
}

// Function to make property cards clickable on the home page
function makeHomePropertiesClickable() {
    const propertyBoxes = document.querySelectorAll('.propertyList .box');
    
    propertyBoxes.forEach((box, index) => {
        // Add cursor pointer style to indicate it's clickable
        box.style.cursor = 'pointer';
        
        // Add click event listener
        box.addEventListener('click', function() {
            // Extract property information from the box
            const propertyTitle = box.querySelector('.titel')?.textContent || '';
            const propertyPrice = box.querySelector('.price')?.textContent || '';
            const propertyLocation = box.querySelector('.location')?.textContent.trim() || '';
            
            // Redirect to property details page with the extracted information
            redirectToPropertyDetails(index + 1, propertyTitle, propertyPrice, propertyLocation);
        });
    });
}

// Function to make property cards clickable on the find property page
function makeFindPropertiesClickable() {
    const propertyItems = document.querySelectorAll('.properties .property');
    
    propertyItems.forEach((property, index) => {
        // Add cursor pointer style to indicate it's clickable
        property.style.cursor = 'pointer';
        
        // Add click event listener
        property.addEventListener('click', function() {
            // Extract property information from the property item
            const propertyPrice = property.querySelector('.price')?.textContent || '';
            const propertyLocation = property.querySelector('.location')?.textContent || '';
            const propertyTitle = `Property ${index + 1}`; // Default title if not available
            
            // Redirect to property details page with the extracted information
            redirectToPropertyDetails(index + 1, propertyTitle, propertyPrice, propertyLocation);
        });
    });
}

// Function to load property details on the property details page
function loadPropertyDetails() {
    // Check if we're on the property details page
    if (window.location.pathname.includes('propertyDetails.html')) {
        // Get the URL parameters
        const urlParams = new URLSearchParams(window.location.search);
        const propertyId = urlParams.get('id');
        const propertyTitle = urlParams.get('title');
        const propertyPrice = urlParams.get('price');
        const propertyLocation = urlParams.get('location');
        
        // Update the page with the property details if they exist
        if (propertyId) {
            // Update property title
            const titleElement = document.querySelector('.property-title');
            if (titleElement && propertyTitle) {
                titleElement.textContent = decodeURIComponent(propertyTitle);
            }
            
            // Update property price
            const priceElement = document.querySelector('.property-price');
            if (priceElement && propertyPrice) {
                priceElement.textContent = decodeURIComponent(propertyPrice);
            }
            
            // Update property location
            const locationElement = document.querySelector('.property-location');
            if (locationElement && propertyLocation) {
                // Keep the icon if it exists
                const icon = locationElement.querySelector('i');
                if (icon) {
                    locationElement.innerHTML = '';
                    locationElement.appendChild(icon);
                    locationElement.innerHTML += ' ' + decodeURIComponent(propertyLocation);
                } else {
                    locationElement.textContent = decodeURIComponent(propertyLocation);
                }
            }
            
            // You could also load different images based on property ID
            // This is a simple example - in a real app, you'd fetch this data from a server
            const mainImage = document.getElementById('mainPropertyImage');
            if (mainImage) {
                mainImage.src = `./images/property-${propertyId}.jpg`;
            }
        }
    }
}

// Function to initialize image navigation on property details page
function initImageNavigation() {
    const prevButton = document.querySelector('.gallery-nav.prev');
    const nextButton = document.querySelector('.gallery-nav.next');
    const thumbnails = Array.from(document.querySelectorAll('.thumbnail-gallery img'));
    
    if (!prevButton || !nextButton || thumbnails.length === 0) return;
    
    // Function to navigate to previous image
    prevButton.addEventListener('click', function() {
        const activeIndex = thumbnails.findIndex(img => img.classList.contains('active'));
        const prevIndex = (activeIndex - 1 + thumbnails.length) % thumbnails.length;
        changeMainImage(thumbnails[prevIndex].src);
    });
    
    // Function to navigate to next image
    nextButton.addEventListener('click', function() {
        const activeIndex = thumbnails.findIndex(img => img.classList.contains('active'));
        const nextIndex = (activeIndex + 1) % thumbnails.length;
        changeMainImage(thumbnails[nextIndex].src);
    });
}

// Initialize the functionality when the DOM is fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Check which page we're on and initialize the appropriate functionality
    if (window.location.pathname.includes('index.html') || window.location.pathname === '/' || window.location.pathname.endsWith('/')) {
        makeHomePropertiesClickable();
    } else if (window.location.pathname.includes('findProperty.html')) {
        makeFindPropertiesClickable();
    } else if (window.location.pathname.includes('propertyDetails.html')) {
        loadPropertyDetails();
        initImageNavigation();
    }
});

// Function to change the main property image
function changeMainImage(src) {
    const mainImage = document.getElementById("mainPropertyImage");
    if (mainImage) {
        mainImage.src = src;
        
        // Update active thumbnail
        const thumbnails = document.querySelectorAll('.thumbnail-gallery img');
        thumbnails.forEach(img => {
            if (img.src === src) {
                img.classList.add('active');
            } else {
                img.classList.remove('active');
            }
        });
    }
}
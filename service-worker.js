const CACHE_NAME = "ics-board-v1";

const ASSETS = [
  ".",                     // current directory
  "index.html",
  "worksheet.png",
  "ICS_Program_Instruction_Manual.txt",
  "icons/ics-192.png",
  "icons/ics-512.png",
  "blank worksheet.png",
  "plain worksheet.png"
];

// Install: cache core assets
self.addEventListener("install", event => {
  event.waitUntil(
    caches.open(CACHE_NAME).then(cache => {
      const urls = ASSETS.map(url => new URL(url, self.location).toString());
      return cache.addAll(urls);
    })
  );
});

// Activate: clean up old caches if version changes
self.addEventListener("activate", event => {
  event.waitUntil(
    caches.keys().then(keys =>
      Promise.all(
        keys
          .filter(key => key !== CACHE_NAME)
          .map(key => caches.delete(key))
      )
    )
  );
});

// Fetch: try cache first, then network
self.addEventListener("fetch", event => {
  event.respondWith(
    caches.match(event.request).then(response => {
      return response || fetch(event.request);
    })
  );
});


# Introduction
I highly recommend watching this <a href="https://youtu.be/yNqjgeMduxU">30s sample video</a> of the project. <br>
This project is an implementation of portals and grapple hook in unity. It was initally intended to be a game, but it was too hard, so it ended up more of a simulation. Portals are not natively supported in unity, that's why it was so difficult. A screenshot from the game: <hr>
![image](https://github.com/Krzyzan42/portals/assets/100627976/657a3b8e-c6ed-4e8b-b663-2c96c6fc5654)

# How was it implemented
Portals are basically just flat planes with textures, that teleport you to the other portal when you enter them. The image on the texture is obtained by rendering the entire world from a camera that's positioned just the right way, so the transition from one portal to another looks seamless. The camera position is adjusted in the code and adequate clipping plane must be set for the camera, otherwise things that are 'behind' the portal get rendered. Then the camera output is saved to the texture, which then gets clipped and mapped onto the portal surface. Portals support recursive images by repeating that process several times, each time with different camera positioning. When player enters a portal, his position and velocity gets transformed.

# A short rant
This was the hardest project of mine so far. So many things can go wrong, there's so much math and many little hacks. Clearly unity wasn't made for such things. Sadly, it ended up not being really usable in a real application, since the portal 'API' is overcomplicated and I don't have the time to fix that.

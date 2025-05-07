# OpenTK Playground

Greetings fellow dev. This is my OpenTK playground, a place where i write stuff with C# and share them with you so you can use them or something.
If i get on a good progress, i might make a full fledged engine out of it :)

# What tools i use or might use:
- OpenTK (The Best C# Wrapper for OpenGL, OpenAL and OpenCL)
- StbiSharp (C# Wrapper for the Stbi Image library - might change it for a C# Wrapped FreeImage library in the future)

# Day 0 - 5/5/2025
![image](https://github.com/user-attachments/assets/6825fd08-b574-4b2e-aa4c-d84908fb5c48)

# Day 1 - 5/6/2025 | Part 1.
![image](https://github.com/user-attachments/assets/aa3b1863-fdb6-4c36-acbb-eeccc5d0e984)
A first mesh was born. His name is Billy the Triangle.

# Day 1 - 5/6/2025 | Part 2.
![image](https://github.com/user-attachments/assets/4d67f694-6b05-4308-9848-67c55113fd33)
Billy grew up quickly and became a somewhat Square.
Also a mention that i expanded the Shader Program code with some Memory Leaks check so i am 100% safe if something goes wrong since my somewhat good checks check everything for me :D
Not only that but the Shader also has some methods for messing around with GLSL uniforms, so this will come in handy when making an MVP (Model * View * Projection) calculation. Off a good start! Thats how i like it.
Also heard of a thing called BufferSubData function, might try it out to see what works and not. might come in handy for Vertex Positions, Normals and UVs.

# Day 3 - 5/7/2025 | Part 1.
![image](https://github.com/user-attachments/assets/fa296814-d16c-4c29-8d92-4a03702b76c5)
Billy became a full blown cube and was finally shifted into the realm of 3D. The cube is optimized since it uses only 8 vertices and 36 indices.
I also prepaired some unfinished UV coordinates for next magic. You can see the code on how i made this madness. It took some trial and error but i manaed to fix a memory leak and some MVP transformation invalidities.
Might also explain why i had such trouble making MVP calculations in my previous OpenGL Attempts. This time i have done it right. Somehow :D

# Day 3 - 5/7/2025 | Part 2.
![image](https://github.com/user-attachments/assets/368e64a1-18e3-4288-8dba-f1e32389dce8)
Billy became a ghostly thin rectangular shape temporarily. I created a basic Transform struct that hold the Positon, Rotation (in euler angles) and Scale. The Transform component calculates the model matrix by a getter making it a simple to approach for the MVP calculation. Later on, we will get moving and get in control with the camera.

const express = require("express");
const path = require("path");

const app = express();

app.use((err, req, res, next) => {
    console.error(err);
})

app.use(express.static(path.resolve(__dirname, "frontend")))

app.get("/*", (req, res) => {
    res.sendFile(path.resolve(__dirname, "frontend", "index.html"));
});


app.listen(process.env.PORT || 9060, () => console.log("Server running"))
export default {
    baseUrl: "https://localhost:44377/",
    constructUrl(path) {
        return this.baseUrl + path
    }
}
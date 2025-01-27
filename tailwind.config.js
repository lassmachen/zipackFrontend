/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],
  theme: {
    extend: {
      fontFamily: {
        sulphur: ['"Sulphur Point"', 'sans-serif'],
        racing: ['"Racing Sans One"', 'sans-serif'],
        staatliches: ['"Staatliches"', 'sans-serif'],
      },
    },
  },
  plugins: [],
}

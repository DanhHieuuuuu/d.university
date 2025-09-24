import localFont from "next/font/local";

export const roboto = localFont({
  src: [
    {
      path: "../../public/fonts/roboto/Roboto-Light.ttf",
      weight: "300",
      style: "normal",
    },
    {
      path: "../../public/fonts/roboto/Roboto-Regular.ttf",
      weight: "400",
      style: "normal",
    },
    {
      path: "../../public/fonts/roboto/Roboto-Italic.ttf",
      weight: "400",
      style: "italic",
    },
    {
      path: "../../public/fonts/roboto/Roboto-Medium.ttf",
      weight: "500",
      style: "normal",
    },
    {
      path: "../../public/fonts/roboto/Roboto-Bold.ttf",
      weight: "700",
      style: "normal",
    },
    {
      path: "../../public/fonts/roboto/Roboto-BoldItalic.ttf",
      weight: "700",
      style: "italic",
    },
  ],
  variable: "--font-roboto",
  display: "swap",
});
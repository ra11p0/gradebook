import "bootstrap/dist/css/bootstrap.css";
import "react-notifications-component/dist/theme.css";
import "animate.css/animate.min.css";
import "react-datepicker/dist/react-datepicker-cssmodules.css";
import "react-datepicker/dist/react-datepicker.css";
import "./Common/Styles/Style.css";
import "tippy.js/dist/tippy.css";
import "tippy.js/animations/scale.css";
import "./i18n/config";
import "moment/locale/pl";
import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { Provider } from "react-redux";
import { store } from "./store";
import { I18nextProvider } from "react-i18next";
import i18n from "./i18n/config";

const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);

root.render(
  <Provider store={store}>
    <I18nextProvider i18n={i18n}>
      <App />
    </I18nextProvider>
  </Provider>
);

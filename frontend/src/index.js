import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import "tabler-react/dist/Tabler.css";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <App />
  //https://legacy.reactjs.org/docs/strict-mode.html
  //strict mode causes double rendering of useEffect in Dev mode and was annoying me
  // <React.StrictMode>
  //   <App />
  // </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

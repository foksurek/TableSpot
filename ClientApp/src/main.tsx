import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './assets/styles/index.scss'
import './assets/styles/singleElements.scss'
import './assets/styles/mobile.scss'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
)

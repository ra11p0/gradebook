import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './Components/Shared/Header';
import Index from './Routes/Index';
import Dashboard from './Routes/Dashboard';

class App extends React.Component{
  render(): React.ReactNode {
    return (
      <div>
        <Header/>
        <BrowserRouter>
          <Routes>
            <Route path='/' element={<Index/>}/>
            <Route path="dashboard" element={<Dashboard />} />
          </Routes>
        </BrowserRouter>
      </div>
    );
  }
}

export default App;

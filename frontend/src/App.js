import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React, { useState, useEffect } from 'react';

import WordCard from './components/word/WordCard';
import Thought from './components/thought/Thought'
import AddThought from './components/thought/AddThought'
import Layout from './components/layout/Layout'
import ThoughtsGrid from './components/thought/ThoughtsGrid';
import WordsGrid from './components/word/WordsGrid';
import Dashboard from './components/dashboard/Dashboard';
import Settings from './components/settings/Settings.jsx';
import ThoughtBucketsGrid from './components/settings/ThoughtBucketsGrid.jsx';
import AddOutdoorActivity from './components/outdoors/AddOutdoorActivity.jsx'
import OutdoorActivityGrid from './components/outdoors/OutdoorActivityGrid.jsx'
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import Concert from './components/music/Concert.jsx';
import RecentThoughts from './components/thought/RecentThoughts.jsx';

const App = () => {
  const [globalSearch, setGlobalSearch] = useState("");  

  const handleGlobalSearchChange = (e) => {
    setGlobalSearch(e.target.value);
  }
  
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
    <Router>
      <Layout className="layout" globalSearchChange={handleGlobalSearchChange}>
        <Routes>
        <Route exact path="/" element={ <Dashboard/> }>
        </Route>
        <Route exact path="/word" element={ <WordCard/> }>
        </Route>
        <Route exact path="/words"  element={ <WordsGrid/>}>
        </Route>
        <Route exact path="/thought"  element={ <Thought/>}>
        </Route>
        <Route exact path="/add-thought"  element={ <AddThought/>}>
        </Route>
        <Route exact path="/recent-thoughts"  element={<RecentThoughts/>}>
        </Route>
        <Route exact path="/organize-thoughts"  element={<ThoughtsGrid globalSearch={globalSearch}/>}>
        </Route>
        <Route exact path="/add-outdoor-activity"  element={ <AddOutdoorActivity/>}>
        </Route>
        <Route exact path="/outdoor-activity-logs"  element={ <OutdoorActivityGrid/>}>
        </Route>
        <Route exact path="/concert"  element={ <Concert/>}>
        </Route>
        <Route exact path="/settings"  element={ <Settings/>}>
        </Route>
        <Route exact path="/settings/thought-buckets"  element={ <ThoughtBucketsGrid/>}>
        </Route>
        </Routes>
        </Layout>
      </Router>
      </LocalizationProvider>
  );
}

export default App;

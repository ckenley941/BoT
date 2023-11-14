import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React from 'react';

import WordCard from './components/word/WordCard';
import Thought from './components/thought/Thought'
import AddThought from './components/thought/AddThought'
import Layout from './components/layout/Layout'
import ThoughtsGrid from './components/thought/ThoughtsGrid';
import WordsGrid from './components/word/WordsGrid';
import Dashboard from './components/dashboard/Dashboard';
import Settings from './components/settings/Settings.jsx';
import ThoughtsCategoriesGrid from './components/settings/ThoughtCategoriesGrid.jsx';

const App = () => {
  return (
    <Router>
      <Layout className="layout">
        <Routes>
        <Route exact path="/" element={ <Dashboard/> }>
        </Route>
        <Route exact path="/word" element={ <WordCard/> }>
        </Route>
        <Route exact path="/thought"  element={ <Thought/>}>
        </Route>
        <Route exact path="/add-thought"  element={ <AddThought/>}>
        </Route>
        <Route exact path="/organize-thoughts"  element={ <ThoughtsGrid/>}>
        </Route>
        <Route exact path="/words"  element={ <WordsGrid/>}>
        </Route>
        <Route exact path="/settings"  element={ <Settings/>}>
        </Route>
        <Route exact path="/settings/thought-categories"  element={ <ThoughtsCategoriesGrid/>}>
        </Route>
        </Routes>
        </Layout>
      </Router>
  );
}

export default App;

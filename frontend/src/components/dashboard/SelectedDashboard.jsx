import React from "react";
import Thought from '../thought/Thought.jsx'
import WordCard from "../word/WordCard.jsx";
import OutdoorActivity from "../outdoors/OutdoorActivity.jsx";

export default function SelectedDashboard( {selected, data} ) {
    return (
        <div className="selectedDashboard">
        {
        {
          'RandomThought': <Thought data= {data} />,
          'RandomWord': <WordCard data= {data} />,
          'RandomOutdoorActivity': <OutdoorActivity data={data}/>,
          '': <div></div>
        }[selected]
      }</div>      
    );
}


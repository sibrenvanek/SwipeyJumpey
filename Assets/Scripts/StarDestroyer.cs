using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDestroyer : MonoBehaviour
{
    void Start()
    {
        ProgressionManager.Instance.ResetCheckpoints();
        Destroy(FindObjectOfType<PauseMenu>().gameObject);
        Destroy(FindObjectOfType<PlayerManager>().gameObject);
        Destroy(FindObjectOfType<GameManager>().gameObject);
        Destroy(FindObjectOfType<CanvasManager>().gameObject);
        Destroy(FindObjectOfType<DialogManager>().gameObject);
        Destroy(FindObjectOfType<ProgressionManager>().gameObject);
    }

    /*
       ______________________________________________________________________
  | .     .               .   .                   .          .           |
  |              . .                     .      ___,_   _         .   .  |
  | .                       .      .          [:t_:::;t"t"+        .     |
  |      .                     .            . `=_ "`[ j.:\=\             |
  |             .      .              .        _,:-.| -"_:\=\  .         |
  |    .           .          .           _,-=":.:%.."+"+|:\=\        .  |
  |          .                   _ _____,:,,;,==.==+nnnpppppppt          |
  |                           _.;-^-._-:._::.'';nn;::m;:%%%%%%%\   .     |
  |  .       .              .;-'_::-:_"--;_:. ((888:(@) ,,;::^%%%,       |
  |                      __='::_:"`::::::::"-;_`YPP::; (d8B((@b."%\     .|
  |      ,------..    __,-:-:::::::::`::`::::::"--;_(@' 88P':^" ;nn:,    |
  |   ,-":%%%%::==.  ;-':::::`%%%\::---:::-:_::::::_"-;_.::((@,(88J::\   |
  |  /:::__ ::%::== """"""""""""""`------`.__.-:::::;___;;::`^__;;;:..7  |
  | /::.'  `.:%%=:=`-=,     . i                   .       """"           |
  |Y:::f    j :%%%%:::=::    ,^.    .        |-|  . .                    |
  |l   `.__+ :::%%%%:::_;[                        |o|                    |
  ||^~'-------------""~:^|                       _` ` _  .. __,,,,+++O#@@#
  |! ::::::::::%%%%==:{                       __j [,,j [#O|||O#@@#O++:|@@#
  | \ `::====: ==== :='            .__,,,++::::.j "  " [%+++::|@##O+::+O##
  |  \:== :: == :=='    __,,,+++|O|. +++..   :::j_[nnj_[_++:+%%_%%|+%|%+O#
  |   "-. =_:::: },+|O##|+::+|:++:::..    ::: .:+%%%%%%j [%O%%j [:+++|++|O
  |   _,,`-------' .+#O#+:||%+ ____   :: .. .:++|O###O%j `'  `' [:::::::++
  |.+:..:++|++||||+.O.::++:|::| _  |:...:++++|||+O##||%j [%..%j [+::LS:+%|
    
    
     */
}
